const template = ({ body }) => {
  return `
        <html>
            <head>
                <title>every-day</title>
            </head>
            <body>
                <div id="root">${body}</div>
            </body>
            <script src="/index.js" charset="utf-8"></script>
            <script src="/vendor.js" charset="utf-8"></script>
        </html>
    `;
};

export default template;
