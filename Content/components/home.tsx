import React, { ReactElement, ReactNode } from 'react';

type Props = {
  children: ReactNode;
};

const Home = ({ children }: Props): ReactElement => {
  return <div>{children}</div>;
};

export default Home;
