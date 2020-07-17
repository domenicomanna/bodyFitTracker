import React from 'react';
import UnauthenticatedLayout from './unauthenticatedLayout/UnauthenticatedLayout';
import About from '../pages/about/About';

const UnauthenticatedApp = () => {
  return (
    <UnauthenticatedLayout>
      <About/>
      {/* Login */}
    </UnauthenticatedLayout>
  );
};

export default UnauthenticatedApp;
