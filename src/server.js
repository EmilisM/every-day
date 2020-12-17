import express from "express";
import index from './routes/index';

const app = express();

app.use('/', index);

const port = process.env.PORT || 3000;

app.listen(port, () => {
    console.info(`Running on ${port}`);
});