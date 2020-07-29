import { string } from 'yup';

export default string()
  .trim('The password can not contain any leading or trailing spaces')
  .min(4, 'Password must be at least 4 characters')
  .strict(true)
  .required('Required');
