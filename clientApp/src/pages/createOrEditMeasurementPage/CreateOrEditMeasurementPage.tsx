import React, { FunctionComponent, useContext, useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { useFormik } from 'formik';
import { object, number, date } from 'yup';
import PageTitle from '../../components/pageTitle/PageTitle';
import { RouteComponentProps } from 'react-router-dom';
import { UserContext } from '../../contexts/UserContext';
import { Gender } from '../../types/userTypes';
import Form from '../../components/ui/form/Form';
import Input from '../../components/ui/input/Input';
import Button from '../../components/ui/button/Button';
import ValidationError from '../../components/ui/validationError/ValidationError';
import { CreateOrEditMeasurement } from '../../types/bodyMeasurementTypes';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';
import routeUrls from '../../constants/routeUrls';
import moment from 'moment';
import { Helmet } from 'react-helmet';
import siteTitle from '../../constants/siteTitle';
import { PageLoader } from '../../components/ui/pageLoader/PageLoader';

function CreateValidationSchema(shouldValidateHipCircumference: boolean) {
  let validationSchema = object<CreateOrEditMeasurement>({
    neckCircumference: number()
      .moreThan(0, 'Must be greater than 0')
      .max(1000, 'Must be less than 1000')
      .required('Required'),
    waistCircumference: number()
      .moreThan(0, 'Must be greater than 0')
      .max(1000, 'Must be less than 1000')
      .required('Required'),
    hipCircumference: !shouldValidateHipCircumference
      ? number()
      : number().moreThan(0, 'Must be greater than 0').max(1000, 'Must be less than 1000').required('Required'),
    height: number().moreThan(0, 'Must be greater than 0').max(1000, 'Must be less than 1000').required('Required'),
    weight: number().moreThan(0, 'Must be greater than 0').max(1000, 'Must be less than 1000').required('Required'),
    dateAdded: date().required('Required').max(new Date(), "Date can't be in the future"),
  });

  return validationSchema;
}

type MeasurementIdToEdit = { measurementIdToEdit: string };

const CreateOrEditMeasurementPage: FunctionComponent<RouteComponentProps<MeasurementIdToEdit>> = ({ match }) => {
  const { gender, measurementPreference, height, userDetailsAreBeingFetched } = useContext(UserContext);

  const defaultFormValues: CreateOrEditMeasurement = {
    neckCircumference: '',
    waistCircumference: '',
    hipCircumference: '',
    height: userDetailsAreBeingFetched ? '' : height,
    weight: '',
    dateAdded: moment().format().split('T')[0],
  };


  const measurementIsBeingCreated: boolean = match.params.measurementIdToEdit ? false : true;
  const history = useHistory();
  const [initialFormValues, setInitialFormValues] = useState<CreateOrEditMeasurement>(defaultFormValues);
  const [measurementToEditIsBeingFetched, setMeasurementToEditIsBeingFetched] = useState(!measurementIsBeingCreated);

  const formik = useFormik({
    initialValues: initialFormValues as CreateOrEditMeasurement,
    validationSchema: CreateValidationSchema(gender === Gender.Female),
    onSubmit: async (createOrEditMeasurementModel) => {
      if (measurementIsBeingCreated) {
        await bodyMeasurementsClient.createMeasurement(createOrEditMeasurementModel);
        history.push(routeUrls.home, { measurementWasCreated: true });
      } else {
        const measurementId: number = parseInt(match.params.measurementIdToEdit);
        await bodyMeasurementsClient.editMeasurement(measurementId, createOrEditMeasurementModel);
        history.push(routeUrls.home, { measurementWasEdited: true });
      }
    },
    enableReinitialize: true,
  });

  useEffect(() => {
    const updateFormBasedOffOfExistingMeasurement = async () => {
      const measurementId: number = parseInt(match.params.measurementIdToEdit);
      const measurement: CreateOrEditMeasurement = await bodyMeasurementsClient.getMeasurement(measurementId);
      setInitialFormValues({
        neckCircumference: measurement.neckCircumference,
        waistCircumference: measurement.waistCircumference,
        hipCircumference: measurement.hipCircumference,
        height: measurement.height,
        weight: measurement.weight,
        dateAdded: measurement.dateAdded,
      });
      setMeasurementToEditIsBeingFetched(false);
    };
    if (measurementIsBeingCreated) {
      setInitialFormValues(defaultFormValues);
    } else {
      updateFormBasedOffOfExistingMeasurement();
    }
  }, [match.params.measurementIdToEdit, userDetailsAreBeingFetched]);

  const titleContent = measurementIsBeingCreated ? 'Create Measurement' : 'Edit Measurement';

  const inputStyle: React.CSSProperties = {
    width: '90%',
  };

  const unitStyle: React.CSSProperties = {
    marginLeft: '.5rem',
  };

  const { weightUnit, lengthUnit } = measurementPreference;

  const hipCircumferenceFields =
    gender === Gender.Male ? null : (
      <>
        <label htmlFor='hipCircumference'>Hip Circumference</label>
        <div>
          <div>
            <Input
              style={inputStyle}
              id='hipCircumference'
              type='number'
              {...formik.getFieldProps('hipCircumference')}
            />
            <span style={unitStyle}>{lengthUnit}</span>
          </div>
          {formik.touched.hipCircumference && formik.errors.hipCircumference ? (
            <ValidationError>{formik.errors.hipCircumference}</ValidationError>
          ) : null}
        </div>
      </>
    );

  const formContent = (
    <Form onSubmit={formik.handleSubmit}>
      <label htmlFor='neckCircumference'>Neck Circumference</label>
      <div>
        <div>
          <Input
            style={inputStyle}
            id='neckCircumference'
            type='number'
            {...formik.getFieldProps('neckCircumference')}
          />
          <span style={unitStyle}>{lengthUnit}</span>
        </div>
        {formik.touched.neckCircumference && formik.errors.neckCircumference ? (
          <ValidationError>{formik.errors.neckCircumference}</ValidationError>
        ) : null}
      </div>

      <label htmlFor='waistCircumference'>Waist Circumference</label>
      <div>
        <div>
          <Input
            style={inputStyle}
            id='waistCircumference'
            type='number'
            {...formik.getFieldProps('waistCircumference')}
          />
          <span style={unitStyle}>{lengthUnit}</span>
        </div>
        {formik.touched.waistCircumference && formik.errors.waistCircumference ? (
          <ValidationError>{formik.errors.waistCircumference}</ValidationError>
        ) : null}
      </div>

      {hipCircumferenceFields}

      <label htmlFor='height'>Height</label>
      <div>
        <div>
          <Input style={inputStyle} id='height' type='number' {...formik.getFieldProps('height')} />
          <span style={unitStyle}>{lengthUnit}</span>
        </div>
        {formik.touched.height && formik.errors.height ? (
          <ValidationError> {formik.errors.height} </ValidationError>
        ) : null}
      </div>

      <label htmlFor='weight'>Weight</label>
      <div>
        <div>
          <Input style={inputStyle} id='weight' type='number' {...formik.getFieldProps('weight')} />
          <span style={unitStyle}>{weightUnit}</span>
        </div>
        {formik.touched.weight && formik.errors.weight ? (
          <ValidationError testId={'weightError'}> {formik.errors.weight} </ValidationError>
        ) : null}
      </div>

      <label htmlFor='date'>Date</label>
      <div>
        <Input style={inputStyle} id='date' type='date' {...formik.getFieldProps('dateAdded')} />
        {formik.touched.dateAdded && formik.errors.dateAdded ? (
          <ValidationError> {formik.errors.dateAdded} </ValidationError>
        ) : null}
      </div>

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
  );

  return (
    <>
      <Helmet>
        <title>
          {siteTitle} | {titleContent}
        </title>
      </Helmet>
      <PageTitle>{titleContent}</PageTitle>
      {measurementToEditIsBeingFetched || userDetailsAreBeingFetched ? <PageLoader testId='pageLoader' /> : formContent}
    </>
  );
};

export default CreateOrEditMeasurementPage;
