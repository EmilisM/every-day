import express from 'express';
import template from '../pages/index';
import App from '../components/app';
import React from 'react';
import { renderToString } from 'react-dom/server';

const router = express.Router();

router.get('/', async (req, res) => {
  const appString = renderToString(<App />);

  res.send(
    template({
      body: appString,
    })
  );
});

export default router;
