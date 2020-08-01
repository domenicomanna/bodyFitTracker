import React, { useContext, useState } from 'react';
import { useFormik } from 'formik';
import PageTitle from '../../components/pageTitle/PageTitle';
import Input from '../../components/ui/input/Input';
import Button from '../../components/ui/button/Button';
import { object, number, string } from 'yup';
import ValidationError from '../../components/ui/validationError/ValidationError';
import Form from '../../components/ui/form/Form';
import { MeasurementSystemName, CreateUserType, Gender, CreateUserResult, User } from '../../types/userTypes';
import RadioButton from '../../components/ui/radioButton/RadioButton';
import RadioButtonGroup from '../../components/ui/radioButtonGroup/RadioButtonGroup';
import usersClient from '../../api/usersClient';
import tokenKey from '../../constants/tokenKey';
import { UserContext } from '../../contexts/UserContext';
import routeUrls from '../../constants/routeUrls';
import { useHistory } from 'react-router-dom';
import { setAuthorizationToken } from '../../api/baseConfiguration';
import password from '../../validationRules/password';
import { Helmet } from 'react-helmet';
import siteTitle from '../../constants/siteTitle';

function CreateValidationSchema() {
  let validationSchema = object({
    email: string().email('Please enter a valid email address').required('Required'),
    password: password,
    confirmedPassword: string()
      .required('Required')
      .test('Password match', 'The passwords do not match', function (value) {
        return this.parent.password === value;
      }),
    height: number().moreThan(0, 'Must be greater than 0').required('Required'),
  });

  return validationSchema;
}

const SignUp = () => {
  const { setEmail, setHeight, setMeasurementPreference, setGender } = useContext(UserContext);
  const [lengthUnit, setLengthUnit] = useState('in');
  const history = useHistory();

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
      confirmedPassword: '',
      height: '',
      unitsOfMeasure: MeasurementSystemName.Imperial,
      gender: Gender.Male,
    } as CreateUserType,
    validationSchema: CreateValidationSchema,
    onSubmit: async (createUserType: CreateUserType) => {
      const createUserResult: CreateUserResult = await usersClient.createUser(createUserType);
      if (!createUserResult.succeeded) {
        if (createUserResult.errors.email) formik.setFieldError('email', createUserResult.errors.email);
      } else {
        localStorage.setItem(tokenKey, createUserResult.token);
        setAuthorizationToken(createUserResult.token);
        const user: User = await usersClient.getUser();
        setHeight(user.height);
        setEmail(user.email);
        setGender(user.gender);
        setMeasurementPreference(user.measurementPreference);
        history.push(routeUrls.home);
      }
    },
  });

  const inputStyle: React.CSSProperties = {
    width: '90%',
  };

  const unitStyle: React.CSSProperties = {
    marginLeft: '.5rem',
  };

  return (
    <>
      <Helmet>
        <title>{siteTitle} | Sign Up</title>
      </Helmet>
      <PageTitle>Sign Up</PageTitle>
      <Form onSubmit={formik.handleSubmit}>
        <label htmlFor='email'>Email</label>
        <div>
          <Input style={inputStyle} id='email' type='email' {...formik.getFieldProps('email')} />
          {formik.touched.email && formik.errors.email && <ValidationError>{formik.errors.email}</ValidationError>}
        </div>

        <label htmlFor='password'>Password</label>
        <div>
          <Input style={inputStyle} id='password' type='password' {...formik.getFieldProps('password')} />
          {formik.touched.password && formik.errors.password && (
            <ValidationError>{formik.errors.password}</ValidationError>
          )}
        </div>

        <label htmlFor='confirmedPassword'>Confirm Password</label>
        <div>
          <Input
            style={inputStyle}
            id='confirmedPassword'
            type='password'
            {...formik.getFieldProps('confirmedPassword')}
          />
          {formik.touched.confirmedPassword && formik.errors.confirmedPassword && (
            <ValidationError>{formik.errors.confirmedPassword}</ValidationError>
          )}
        </div>

        <label htmlFor='height'>Height</label>
        <div>
          <div>
            <Input style={inputStyle} id='height' type='number' {...formik.getFieldProps('height')} />
            <span data-testid='lengthUnit' style={unitStyle}>
              {lengthUnit}
            </span>
          </div>
          {formik.touched.height && formik.errors.height && <ValidationError>{formik.errors.height}</ValidationError>}
        </div>

        <span>Units of Measure</span>
        <RadioButtonGroup>
          <RadioButton
            name='unitsOfMeasure'
            checked={formik.values.unitsOfMeasure === MeasurementSystemName.Imperial}
            onChange={() => {
              formik.setFieldValue('unitsOfMeasure', MeasurementSystemName.Imperial);
              setLengthUnit('in');
            }}
            displayText='Imperial'
          />
          <RadioButton
            name='unitsOfMeasure'
            checked={formik.values.unitsOfMeasure === MeasurementSystemName.Metric}
            onChange={() => {
              formik.setFieldValue('unitsOfMeasure', MeasurementSystemName.Metric);
              setLengthUnit('cm');
            }}
            displayText='Metric'
          />
        </RadioButtonGroup>

        <span>Gender</span>
        <RadioButtonGroup>
          <RadioButton
            id='gender'
            name='gender'
            checked={formik.values.gender === Gender.Male}
            onChange={() => formik.setFieldValue('gender', Gender.Male)}
            displayText='Male'
          />
          <RadioButton
            name='gender'
            checked={formik.values.gender === Gender.Female}
            onChange={() => formik.setFieldValue('gender', Gender.Female)}
            displayText='Female'
          />
        </RadioButtonGroup>

        <span></span>
        <Button
          style={inputStyle}
          buttonClass='primary'
          disabled={!formik.isValid || formik.isSubmitting || !formik.dirty}
          type='submit'
          isSubmitting={formik.isSubmitting}
        >
          Submit
        </Button>
      </Form>
    </>
  );
};

export default SignUp;
