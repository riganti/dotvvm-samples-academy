module.exports = (context) => ({
    map: context.options.map,
    parser: context.options.parser,
    plugins: [
        require('autoprefixer')({ grid: true, flex: true }),
    ]
})
