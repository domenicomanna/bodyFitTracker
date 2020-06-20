import React, { FunctionComponent, useContext, Fragment, useEffect, useState } from 'react';
import { useFormik } from 'formik';
import { string, object, number } from 'yup';
import PageTitle from '../../components/pageTitle/PageTitle';
import { RouteComponentProps } from 'react-router-dom';
import { UserContext } from '../../contexts/UserContext';
import { Gender } from '../../models/userModels';
import Form from '../../components/ui/form/Form';
import Input from '../../components/ui/input/Input';
import Button from '../../components/ui/button/Button';
import FieldValidationError from '../../components/ui/fieldValidationError/FieldValidationError';

type CreateOrEditFormValues = {
  neckCircumference: string | number;
  waistCircumference: string | number;
  hipCircumference?: string | number;
  weight: string | number;
};

function CreateValidationSchema(shouldValidateHipCircumference: boolean) {
  let validationSchema = object<CreateOrEditFormValues>({
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
  });

  return validationSchema;
}

type MeasurementIdToEdit = { measurementIdToEdit: string };

const CreateOrEditMeasurementPage: FunctionComponent<RouteComponentProps<MeasurementIdToEdit>> = ({ match }) => {
  const { gender } = useContext(UserContext);

  const measurementIsBeingCreated: boolean = match.params.measurementIdToEdit ? false : true;
  const pageTitleContent = measurementIsBeingCreated ? 'Create Measurement' : 'Edit Measurement';

  const [formIsSubmitting, setFormIsSubmitting] = useState(false);
  const [initialFormValues, setInitialFormValues] = useState<CreateOrEditFormValues>({
    neckCircumference: '',
    waistCircumference: '',
    hipCircumference: '',
    weight: '',
  });

  const formik = useFormik({
    initialValues: initialFormValues as CreateOrEditFormValues,
    validationSchema: CreateValidationSchema(gender == Gender.Female),
    onSubmit: async (values) => {
      alert(JSON.stringify(values, null, 2));
      setFormIsSubmitting(true);
      setTimeout(() => {
        setFormIsSubmitting(false);
      }, 4000);
    },
    validateOnMount: measurementIsBeingCreated,
    enableReinitialize: true,
  });

  // useEffect(() => {
  //   setInitialFormValues({
  //     neckCircumference: '19',
  //     waistCircumference: '20',
  //     hipCircumference: '30',
  //     weight: '120',
  //   })
  //   formik.initialValues = initialFormValues as CreateOrEditFormValues
  // }, []);

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
            <FieldValidationError> {formik.errors.weight} </FieldValidationError>
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
