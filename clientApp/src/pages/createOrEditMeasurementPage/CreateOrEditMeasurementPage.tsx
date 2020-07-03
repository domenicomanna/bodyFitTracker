import React, { FunctionComponent, useContext, Fragment, useEffect, useState } from 'react';
import { useFormik } from 'formik';
import { object, number, date } from 'yup';
import PageTitle from '../../components/pageTitle/PageTitle';
import { RouteComponentProps } from 'react-router-dom';
import { UserContext } from '../../contexts/UserContext';
import { Gender } from '../../models/userModels';
import Form from '../../components/ui/form/Form';
import Input from '../../components/ui/input/Input';
import Button from '../../components/ui/button/Button';
import FieldValidationError from '../../components/ui/fieldValidationError/FieldValidationError';
import { CreateOrEditMeasurementModel } from '../../models/bodyMeasurementModels';
import bodyMeasurementsClient from '../../api/bodyMeasurementsClient';

function CreateValidationSchema(shouldValidateHipCircumference: boolean) {
  let validationSchema = object<CreateOrEditMeasurementModel>({
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
    creationDate: date().required('Required').max(new Date(), "Date can't be in the future"),
  });

  return validationSchema;
}

type MeasurementIdToEdit = { measurementIdToEdit: string };

const CreateOrEditMeasurementPage: FunctionComponent<RouteComponentProps<MeasurementIdToEdit>> = ({ match }) => {
  const { gender, measurementPreference } = useContext(UserContext);

  const measurementIsBeingCreated: boolean = match.params.measurementIdToEdit ? false : true;
  const pageTitleContent = measurementIsBeingCreated ? 'Create Measurement' : 'Edit Measurement';

  const [formIsSubmitting, setFormIsSubmitting] = useState(false);
  const [initialFormValues, setInitialFormValues] = useState<CreateOrEditMeasurementModel>({
    neckCircumference: '',
    waistCircumference: '',
    hipCircumference: '',
    height: '',
    weight: '',
    creationDate: new Date().toISOString().split('T')[0],
  });

  const formik = useFormik({
    initialValues: initialFormValues as CreateOrEditMeasurementModel,
    validationSchema: CreateValidationSchema(gender == Gender.Female),
    onSubmit: async (createOrEditMeasurementModel) => {
      console.log(createOrEditMeasurementModel);
      setFormIsSubmitting(true);
      if (measurementIsBeingCreated) await bodyMeasurementsClient.createMeasurement(createOrEditMeasurementModel);
      setFormIsSubmitting(false);
    },
    validateOnMount: measurementIsBeingCreated,
    enableReinitialize: true,
  });

  useEffect(() => {
    if (!measurementIsBeingCreated) {
      setInitialFormValues({
        neckCircumference: '19',
        waistCircumference: '20',
        hipCircumference: '30',
        height: '60',
        weight: '120',
        creationDate: '2020-06-19',
      });
      formik.initialValues = initialFormValues as CreateOrEditMeasurementModel;
    }
  }, []);

  const inputStyle: React.CSSProperties = {
    width: '90%',
  };

  const unitStyle: React.CSSProperties = {
    marginLeft: '.5rem',
  };

  const { weightUnit, lengthUnit } = measurementPreference;

  const hipCircumferenceFields =
    gender == Gender.Male ? null : (
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
            <FieldValidationError>{formik.errors.hipCircumference}</FieldValidationError>
          ) : null}
        </div>
      </>
    );

  return (
    <>
      <PageTitle>{pageTitleContent}</PageTitle>
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
            <FieldValidationError>{formik.errors.neckCircumference}</FieldValidationError>
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
            <FieldValidationError>{formik.errors.waistCircumference}</FieldValidationError>
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
            <FieldValidationError> {formik.errors.height} </FieldValidationError>
          ) : null}
        </div>

        <label htmlFor='weight'>Weight</label>
        <div>
          <div>
            <Input style={inputStyle} id='weight' type='number' {...formik.getFieldProps('weight')} />
            <span style={unitStyle}>{weightUnit}</span>
          </div>
          {formik.touched.weight && formik.errors.weight ? (
            <FieldValidationError testId={'weight'}> {formik.errors.weight} </FieldValidationError>
          ) : null}
        </div>

        <label htmlFor='date'>Date</label>
        <div>
          <Input style={inputStyle} id='date' type='date' {...formik.getFieldProps('creationDate')} />
          {formik.touched.creationDate && formik.errors.creationDate ? (
            <FieldValidationError> {formik.errors.creationDate} </FieldValidationError>
          ) : null}
        </div>

        <span></span>
        <Button
          style={inputStyle}
          buttonClass='primary'
          disabled={!formik.isValid || formIsSubmitting}
          type='submit'
          isSubmitting={formIsSubmitting}
        >
          Submit
        </Button>
      </Form>
    </>
  );
};

export default CreateOrEditMeasurementPage;
