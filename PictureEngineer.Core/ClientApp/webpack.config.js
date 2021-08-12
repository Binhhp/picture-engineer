const TARGET = process.env.npm_lifecycle_event;
const configProduction = require('./configs/webpack.config.prod');
//convert env ->dotenv in webpack
var dotenv = require('dotenv').config({path: __dirname + '/.env'});
const webpack = require('webpack');

if (TARGET === 'build:dev' || TARGET === 'dev' || !TARGET) {
    module.exports = require('./configs/webpack.config.dev');
    console.info('======================================************************================================================');
    console.info('======================================> MODE [ DEVELOPMENT ] <================================================');
    console.info('======================================************************================================================');
}

if(TARGET === 'build'){
    module.exports = {
        ...configProduction,
        plugins: [
            ...configProduction.plugins,
            //webpack file .env
            new webpack.DefinePlugin({
                "process.env": JSON.stringify(dotenv.parsed)
            }),
        ]
    };
    console.info('======================================************************================================================');
    console.info('======================================> MODE [ PRODUCTION ] <================================================');
    console.info('======================================************************================================================');
}