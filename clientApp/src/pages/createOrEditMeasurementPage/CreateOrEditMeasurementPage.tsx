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
    weight: number().moreThan(0, 'Must be greater than 0').max(1000, 'Must be less than 1000').required('Required'),
    creationDate: date().required('Required').max(new Date(), "Date can't be in the future"),
  });

  return validationSchema;
}

type MeasurementIdToEdit = { measurementIdToEdit: string };

const CreateOrEditMeasurementPage: FunctionComponent<RouteComponentProps<MeasurementIdToEdit>> = ({ match }) => {
  const { gender } = useContext(UserContext);

  const measurementIsBeingCreated: boolean = match.params.measurementIdToEdit ? false : true;
  const pageTitleContent = measurementIsBeingCreated ? 'Create Measurement' : 'Edit Measurement';

  const [formIsSubmitting, setFormIsSubmitting] = useState(false);
  const [initialFormValues, setInitialFormValues] = useState<CreateOrEditMeasurementModel>({
    neckCircumference: '',
    waistCircumference: '',
    hipCircumference: '',
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
        weight: '120',
        creationDate: '2020-06-19',
      });
      formik.initialValues = initialFormValues as CreateOrEditMeasurementModel;
    }
  }, []);

  const hipCircumferenceFields =
    gender == Gender.Male ? null : (
      <>
        <label htmlFor='hipCircumference'>Hip Circumference</label>
        <div>
          <Input id='hipCircumference' type='number' {...formik.getFieldProps('hipCircumference')} />
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
          <Input id='neckCircumference' type='number' {...formik.getFieldProps('neckCircumference')} />
          {formik.touched.neckCircumference && formik.errors.neckCircumference ? (
            <FieldValidationError>{formik.errors.neckCircumference}</FieldValidationError>
          ) : null}
        </div>

        <label htmlFor='waistCircumference'>Waist Circumference</label>
        <div>
          <Input id='waistCircumference' type='number' {...formik.getFieldProps('waistCircumference')} />
          {formik.touched.waistCircumference && formik.errors.waistCircumference ? (
            <FieldValidationError>{formik.errors.waistCircumference}</FieldValidationError>
          ) : null}
        </div>

        {hipCircumferenceFields}

        <label htmlFor='weight'>Weight</label>
        <div>
          <Input id='weight' type='number' {...formik.getFieldProps('weight')} />
          {formik.touched.weight && formik.errors.weight ? (
            <FieldValidationError testId={'weight'}> {formik.errors.weight} </FieldValidationError>
          ) : null}
        </div>

        <label htmlFor='date'>Date</label>
        <div>
          <Input id='date' type='date' {...formik.getFieldProps('creationDate')} />
          {formik.touched.creationDate && formik.errors.creationDate ? (
            <FieldValidationError> {formik.errors.creationDate} </FieldValidationError>
          ) : null}
        </div>

        <span></span>
        <Button
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
