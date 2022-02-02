import React, { Suspense, lazy } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import MainLayout from './layout/MainLayout';
import RouteWithSubRoutes from './RouteWithSubRoutes';

const Home = lazy(() => import('pages/home/Home'));
const MatchList = lazy(() => import('pages/list/MatchList'));

const routes = [
  {
    path: '/',
    component: Home,
  },
  {
    path: '/list',
    component: MatchList,
  }
];

const AppRouter = () => {
  return (
    <Router>
      <MainLayout>
        <Suspense fallback={<div className="lazy-loading">Loading...</div>}>
          {routes.map((route, i) => (
            <RouteWithSubRoutes key={i} {...route} />
          ))}
        </Suspense>
      </MainLayout>
    </Router>
  );
};

export default AppRouter;
