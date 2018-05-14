module.exports = (context) => ({
    map: context.options.map,
    parser: context.options.parser,
    plugins: [
        require('./postcss-image-set.js'),
        require('autoprefixer')({browsers: "last 3 versions"}),
    ]
})
