import express from 'express';
import index from './src/routes/index';

var webpack = require('webpack');
var webpackConfig = require('./webpack.config');
var compiler = webpack(webpackConfig);

const devMiddleware = require('webpack-dev-middleware');

const app = express();

app.use(
  devMiddleware(compiler, {
    publicPath: webpackConfig.output.publicPath,
  })
);

app.use('/', index);

const port = process.env.PORT || 3000;

app.listen(port, () => {
  console.info(`Running on ${port}`);
});
