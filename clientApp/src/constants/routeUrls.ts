const routeUrls = {
  home: '/',
  editMeasurement: '/edit-measurement/:measurementIdToEdit(\\d+)',
  editMeasurementWithoutRouteParameter: '/edit-measurement',
  createMeasurement: '/create-measurement',
  changePassword: '/change-password',
  editAccount: '/edit-account',
  login: '/login',
  signUp: '/signUp',
  about: '/about',
  notFound: '/not-found',
  serverError: '/server-error',
  resetPassword:{
    stepOne: '/reset-password-step-one',
    stepOneSuccess: '/reset-password-email-sent',
    stepTwo: '/reset-password-step-two/:token',
    invalidToken: '/reset-password-invalid-token'
  }
};

export default routeUrls;
