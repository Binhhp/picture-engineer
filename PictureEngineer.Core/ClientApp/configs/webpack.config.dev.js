const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { WebpackManifestPlugin } = require('webpack-manifest-plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

const PATHS = {
    src: path.join(__dirname, '../src/index.js'),
    build: path.join(__dirname, '../build')
};

console.info(PATHS.src);

module.exports = {
    entry: {
        main: ['@babel/polyfill', PATHS.src]
    },
    
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-react']
                    }
                },
            },
            {
                test: /\.css$/i,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            },
            {
                test: /\.(jpg|png)$/,
                use: {
                    loader: "url-loader",
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
        new BundleAnalyzerPlugin(),
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
            inject: 'body'
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
        historyApiFallback: true,
        headers: {
            'X-Content-Type-Options': 'nosniff',
            'X-Frame-Options': 'DENY'
        },
        overlay: {
            warnings: true,
            errors: true
        },
    },

    optimization: {
        runtimeChunk: 'single', //enable "runtime" chunk
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

    stats: {
        children: false
    }
};