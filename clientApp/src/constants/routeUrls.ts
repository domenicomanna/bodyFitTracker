const routeUrls = {
  home: '/',
  editMeasurement: '/edit-measurement/:measurementIdToEdit(\\d+)',
  editMeasurementWithoutRouteParameter: '/edit-measurement',
  createMeasurement: '/create-measurement',
  changePassword: '/change-password',
  login: '/login',
  signUp: '/signUp',
  about: '/about',
  notFound: '/not-found',
  resetPassword:{
    stepOne: '/reset-password-step-one',
    stepOneSuccess: '/reset-password-email-sent',
    stepTwo: '/reset-password-step-two',
  }
};

export default routeUrls;
