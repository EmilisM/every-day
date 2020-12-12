import { ReactElement } from 'react';
import { Link } from 'react-router-dom';

export const Navbar = (): ReactElement => {
  return (
    <div>
      <Link to="/">Home</Link>
      <Link to="/about">About</Link>
    </div>
  );
};

export default Navbar;
