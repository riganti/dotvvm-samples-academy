'use strict';

const postcss = require('postcss');
const mediaParser = require('postcss-media-query-parser').default;
const valueParser = require('postcss-value-parser');

const DPI_RATIO = {
    x: 96,
    dppx: 96,
    dpcm: 2.54,
    dpi: 1,
    w: 1
};

// convert all sizes to dpi for sorting
const convertSize = (value, decl) => {

    const size = stringify(value);

    if (!size) {
        return DPI_RATIO.x;
    }

    const m = size.match(/^([0-9|\.]+)(.*?)$/);

    if (m && DPI_RATIO[m[2]]) {

        if (value.value.endsWith('w')) {
            return {
                unit: 'px',
                value: m[1]
            };
        }

        const dpi = m[1] * DPI_RATIO[m[2]];

        return {
            unit: 'dpi',
            value: Math.floor(dpi),
            pxRatio: Math.floor(dpi / DPI_RATIO.x * 100) / 100
        };
    }

    throw decl.error('Incorrect size value', { word: m && m[2] });
};

const stringify = chunk => valueParser.stringify(chunk);

const parseValue = (value, decl) => {
    const valueChunks = valueParser(value).nodes;

    const imageSetChunks = valueChunks.shift().nodes;

    const sizes = imageSetChunks
        .filter(chunk => chunk.type === 'word')
        .map(chunk => convertSize(chunk, decl));

    const urls = imageSetChunks
        .filter(chunk => chunk.type === 'function' || chunk.type === 'string')
        .map(chunk => {
            const str = stringify(chunk);
            return chunk.type === 'string' ? `url(${str})` : str;
        });

    const suffix = valueChunks.length ?
        valueChunks
            .map(stringify)
            .join('') :
        '';

    return {
        images: {
            size: sizes,
            url: urls
        },
        suffix
    };
};

module.exports = postcss.plugin('postcss-image-set-polyfill', () =>
    css => {
        css.walkDecls(/^(background-image|background)$/, decl => {
            // ignore nodes we already visited
            if (decl.__visited) {
                return;
            }

            // make sure we have image-set
            if (!decl.value || decl.value.indexOf('image-set') === -1) {
                return;
            }

            const commaSeparatedValues = postcss.list.comma(decl.value);
            const mediaQueryList = {};

            const parsedValues = commaSeparatedValues.map(value => {
                const result = {};

                if (value.indexOf('image-set') === -1) {
                    result.default = value;
                    return result;
                }

                const parsedValue = parseValue(value, decl);
                const images = parsedValue.images;
                const suffix = parsedValue.suffix;

                result.default = images.url[0] + suffix;

                // for each image add a media query
                if (images.url.length > 1) {
                    for (let i = 0, len = images.url.length; i < len; i++) {

                        const size = images.size[i].value;

                        if (size === DPI_RATIO.x) {
                            result.default = images.url[i] + suffix;
                        } else {
                            if (!mediaQueryList[size]) {
                                mediaQueryList[size] = images.size[i];
                            }
                            result[size] = images.url[i] + suffix;
                        }
                    }
                }

                return result;
            });

            // add the default image to the decl
            decl.value = parsedValues.map(val => val.default).join(',');

            // check for the media queries
            const media = decl.parent.parent.params;
            const parsedMedia = media && mediaParser(media);

            Object.keys(mediaQueryList)
                .sort((a, b) => a.value > b.value)
                .forEach(index => {
                    const size = mediaQueryList[index];
                    let paramStr;
                    if (size.unit === 'px') {
                        const maxResQuery = `only screen and (max-width: ${size.value}${size.unit})`;

                        paramStr = parsedMedia ?
                            parsedMedia.nodes
                            .map(queryNode => `${queryNode.value} and ${queryNode.value} and ${maxResQuery}`)
                            .join(',') :
                            `${maxResQuery}`;
                    }
                    else {
                        const minResQuery = `(min-resolution: ${size.value}${size.unit})`;
                        const minDPRQuery = `(-webkit-min-device-pixel-ratio: ${size.pxRatio})`;

                        paramStr = parsedMedia ?
                            parsedMedia.nodes
                                .map(queryNode => `${queryNode.value} and ${minDPRQuery}, ${queryNode.value} and ${minResQuery}`)
                                .join(',') :
                            `${minDPRQuery}, ${minResQuery}`;
                    }

                    const atrule = postcss.atRule({
                        name: 'media',
                        params: paramStr
                    });

                    // clone empty parent with only relevant decls
                    const parent = decl.parent.clone({
                        nodes: []
                    });

                    const d = decl.clone({
                        value: parsedValues.map(val => val[size.value] || val.default).join(',')
                    });

                    // mark nodes as visited by us
                    d.__visited = true;

                    parent.append(d);
                    atrule.append(parent);

                    decl.root().append(atrule);
                });
        });
    }
);


