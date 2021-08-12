const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const RobotstxtPlugin = require("robotstxt-webpack-plugin");
const SitemapPlugin = require('sitemap-webpack-plugin').default;
const { WebpackManifestPlugin } = require('webpack-manifest-plugin');
//set path entry and output
const PATHS = {
    src: path.join(__dirname, '../src/index.js'),
    build: path.join(__dirname, '../build')
};
//setting sitemap
const options = {
    sitemap: "https://binhhp.github.io/picture-engineer/sitemap.txt"
};

// Example of simple string paths
const paths = [
    'https://binhhp.github.io/picture-engineer/'
];

module.exports = {
    entry: ['@babel/polyfill', PATHS.src],
    
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: ['babel-loader'],
            },
            {
                test: /\.css$/i,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            },
            {
                test: /\.(jpg|png)$/,
                use: {
                    loader: "url-loader",
                    options: {
                        // Inline files smaller than 10 kB (10240 bytes)
                        limit: 10 * 1024,
                    },
                },
            },
            {
                test: /\.woff(2)?(\?[a-z0-9]+)?$/,
                use: {
                    loader: "url-loader?limit=10000&mimetype=application/font-woff"
                }
            }, 
            {
                test: /\.(ttf|eot|svg)(\?[a-z0-9]+)?$/,
                use: {
                    loader: "file-loader",
                    options: {
                        // Inline files smaller than 10 kB (10240 bytes)
                        limit: 10 * 1024,
                        // Remove the quotes from the url
                        // (they’re unnecessary in most cases)
                        noquotes: true,
                    },
                },
            }
        ],
    },

    resolve: {
        extensions: ['*', '.js', '.jsx'],
    },
    
    output: {
        path: PATHS.build,
        filename: '[name]-[hash:6].bundle.js',
        chunkFilename: "[name]-[hash:6].bundle.js",
        publicPath: '/'
    },

    plugins: [
        new webpack.HotModuleReplacementPlugin(),
        //webpack css
        new MiniCssExtractPlugin({
            linkType: false,
            filename: '[name].css',
            chunkFilename: 'style-[id].css',
        }),
        //webpack html
        new HtmlWebpackPlugin({
            template: './public/index.html',
            favicon: './public/favicon.ico',
            filename: 'index.html',
            minify: {
                collapseWhitespace: true,
                conservativeCollapse: true,
                preserveLineBreaks: true,
                useShortDoctype: true,
                html5: true
            },
            mobile: true,
            base: { 'href': '/www.pengineer.ml/'},
            inject: 'body'
        }),
        new HtmlWebpackPlugin({
            template: './public/404.html',
            filename: '404.html',
            inject: 'body'
        }),
        //webpack file robots
        new RobotstxtPlugin(options),
        //webpack file sitemap
        new SitemapPlugin({
            base: "https://binhhp.github.io/picture-engineer/",
            paths
        }),
        //know the generated name of the runtime chunk
        new WebpackManifestPlugin({
            fileName: './manifest.json',
            publicPath: '/static/react/',
        }),
    ],

    devServer: {
        contentBase: path.resolve(__dirname, './build'),
        hot: true,
        open: true,
        watchContentBase: true,
        historyApiFallback: true
    },

    optimization: {
        runtimeChunk: 'single',//enable "runtime" chunk
        splitChunks: {
            cacheGroups: {
                reactDomVendor: {
                    test: /[\\/]node_modules[\\/](react-dom)[\\/]/,
                    name: "dom-vendor",
                    chunks: 'all',
                    maxInitialRequests: Infinity,
                    minSize: 0, 
                },
                reactVendor: {
                    test: /[\\/]node_modules[\\/](react|react-router-dom)[\\/]/,
                    name: "react-vendor",
                    chunks: 'all',
                    maxInitialRequests: Infinity,
                    minSize: 0, 
                },
                bootstrapVendor: {
                    test: /[\\/]node_modules[\\/](react-bootstrap)[\\/]/,
                    name: "bootstrap-vendor",
                    chunks: 'all',
                    maxInitialRequests: Infinity,
                    minSize: 0, 
                },
                vendor: {
                    test: /[\\/]node_modules[\\/](!react-bootstrap)(!react-router-dom)(!react)(!react-dom)[\\/]/,
                    name: "vendor",
                    chunks: 'all',
                    maxInitialRequests: Infinity,
                    minSize: 0, 
                },
            }
        }
    },
};