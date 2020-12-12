import React, { ReactElement } from 'react';
import { BrowserRouter, Route, StaticRouter, Switch } from 'react-router-dom';
import Navbar from './components/Navbar';
import About from './pages/about';
import Home from './pages/home';

const App = (): ReactElement | null =>
  typeof window !== 'undefined' ? (
    <BrowserRouter>
      <Navbar />
      <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/about" component={About} />
      </Switch>
    </BrowserRouter>
  ) : null;

export default App;
