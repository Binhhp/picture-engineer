const { BundleAnalyzerPlugin } = require('webpack-bundle-analyzer');
const config = require('./webpack.config.prod');
//webpack-bundle-analyzer to check content of scripts
module.exports = {
    ...config,
    plugins: [
        ...config.plugins,
        new BundleAnalyzerPlugin({
            analyzerMode: 'static',
            openAnalyzer: true,
            generateStatsFile: true
        })
    ]
};