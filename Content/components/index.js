import React from "react";
import ReactDOM from "react-dom";
import ReactDOMServer from "react-dom/server";

import Home from "./home";

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;

global.Components = { Home };
