import _React from "react";
import _ReactDOM from "react-dom";
import _ReactDOMServer from "react-dom/server";

declare global {
  const React: typeof _React;
  const ReactDOM: typeof _ReactDOM;
  const ReactDOMServer: typeof _ReactDOMServer;
}
