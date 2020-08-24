import React, { useContext, useState, useEffect } from 'react';
import { useFormik } from 'formik';
import PageTitle from '../../components/pageTitle/PageTitle';
import Input from '../../components/ui/input/Input';
import Button from '../../components/ui/button/Button';
import { object, number, string, mixed } from 'yup';
import ValidationError from '../../components/ui/validationError/ValidationError';
import Form from '../../components/ui/form/Form';
import { MeasurementSystemName, EditAccountType, MeasurementPreference } from '../../types/userTypes';
import RadioButton from '../../components/ui/radioButton/RadioButton';
import RadioButtonGroup from '../../components/ui/radioButtonGroup/RadioButtonGroup';
import usersClient from '../../api/usersClient';
import { UserContext } from '../../contexts/UserContext';
import routeUrls from '../../constants/routeUrls';
import { useHistory } from 'react-router-dom';
import { Helmet } from 'react-helmet';
import brandName from '../../constants/brandName';
import { PageLoader } from '../../components/ui/pageLoader/PageLoader';
import { measurementConverter } from '../../utils/measurementConversion/measurementConverter';
import { BodyMeasurementsPageLocationState } from '../bodyMeasurementsPage/BodyMeasurementsPage';

function CreateValidationSchema() {
  let validationSchema = object<EditAccountType>({
    email: string().email('Please enter a valid email address').required('Required'),
    height: number().moreThan(0, 'Must be greater than 0').required('Required'),
    unitsOfMeasure: mixed<MeasurementSystemName>().oneOf(Object.values(MeasurementSystemName)),
  });

  return validationSchema;
}

const EditAccount = () => {
  const {
    email,
    height,
    measurementPreference,
    setEmail,
    setHeight,
    setMeasurementPreference,
    userDetailsAreBeingFetched,
  } = useContext(UserContext);

  const [lengthUnit, setLengthUnit] = useState<string>(measurementPreference.lengthUnit);
  const history = useHistory();
  const [initialFormValues, setInitialFormValues] = useState<EditAccountType>({
    email: userDetailsAreBeingFetched ? '' : email,
    height: userDetailsAreBeingFetched ? '' : height.toFixed(2),
    unitsOfMeasure: userDetailsAreBeingFetched
      ? MeasurementSystemName.Imperial
      : measurementPreference.measurementSystemName,
  });

  const formik = useFormik({
    initialValues: initialFormValues as EditAccountType,
    validationSchema: CreateValidationSchema,
    enableReinitialize: true,
    onSubmit: async (formValues: EditAccountType) => {
      await usersClient.changeProfileSettings(formValues);
      setEmail(formValues.email);
      setHeight(Number(formValues.height));
      const updatedMeasurementPreference: MeasurementPreference = {
        measurementSystemName: formValues.unitsOfMeasure,
        lengthUnit: formValues.unitsOfMeasure === MeasurementSystemName.Imperial ? 'in' : 'cm',
        weightUnit: formValues.unitsOfMeasure === MeasurementSystemName.Imperial ? 'lb' : 'kg',
      };
      setMeasurementPreference(updatedMeasurementPreference);
      const locationState: BodyMeasurementsPageLocationState = {
        profileSettingsUpdated: true,
      };
      history.push(routeUrls.home, locationState);
    },
  });

  useEffect(() => {
    if (userDetailsAreBeingFetched) return;
    setInitialFormValues({
      email: email,
      height: height.toFixed(2),
      unitsOfMeasure: measurementPreference.measurementSystemName,
    });
    setLengthUnit(measurementPreference.lengthUnit);
  }, [userDetailsAreBeingFetched]);

  const inputStyle: React.CSSProperties = {
    width: '90%',
  };

  const unitStyle: React.CSSProperties = {
    marginLeft: '.5rem',
  };

  const formContent = (
    <Form onSubmit={formik.handleSubmit}>
      <label htmlFor='email'>Email</label>
      <div>
        <Input style={inputStyle} id='email' type='email' {...formik.getFieldProps('email')} />
        {formik.touched.email && formik.errors.email && <ValidationError>{formik.errors.email}</ValidationError>}
      </div>

      <label htmlFor='height'>Height</label>
      <div>
        <div>
          <Input style={inputStyle} id='height' type='number' {...formik.getFieldProps('height')} />
          <span style={unitStyle}>
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
            if (formik.values.height.toString().trim() === '') return;
            const heightInCentimeters: number = formik.values.height as number;
            const heightConvertedToInches: string = measurementConverter
              .convertToInches(heightInCentimeters)
              .toFixed(2);
            formik.setFieldValue('height', heightConvertedToInches);
          }}
          displayText='Imperial'
        />
        <RadioButton
          name='unitsOfMeasure'
          checked={formik.values.unitsOfMeasure === MeasurementSystemName.Metric}
          onChange={() => {
            formik.setFieldValue('unitsOfMeasure', MeasurementSystemName.Metric);
            setLengthUnit('cm');
            if (formik.values.height.toString().trim() === '') return;
            const heightInInches: number = formik.values.height as number;
            const heightConvertedToCentimeters: string = measurementConverter
              .convertToCentimeters(heightInInches)
              .toFixed(2);
            formik.setFieldValue('height', heightConvertedToCentimeters);
          }}
          displayText='Metric'
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
        Edit Settings
      </Button>
    </Form>
  );

  return (
    <>
      <Helmet>
        <title>{brandName} | Edit Settings</title>
      </Helmet>
      <PageTitle>Edit Settings</PageTitle>
      {userDetailsAreBeingFetched ? <PageLoader /> : formContent}
    </>
  );
};

export default EditAccount;
