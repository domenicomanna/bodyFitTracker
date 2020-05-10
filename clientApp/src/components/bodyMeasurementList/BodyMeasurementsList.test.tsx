import React from 'react'
import { render, screen} from "@testing-library/react";
import BodyMeasurementList from './BodyMeasurementList';
import {BodyMeasurementCollectionModel, BodyMeasurementModel} from '../../models/bodyMeasurementModels';
import BodyMeasurement from '../bodyMeasurement/BodyMeasurement';

let bodyMeasurementCollection: BodyMeasurementCollectionModel;
let bodyMeasurement: BodyMeasurementModel;

beforeEach(() => {
    bodyMeasurementCollection = {
        measurementSystemName : "",
        genderTypeName : "",
        length : {
            name : "",
            abbreviation : "",
        },
        weight : {
            name : "",
            abbreviation : ""
        },
        bodyMeasurements : []
    };

    bodyMeasurement = {
        bodyMeasurementId : 1,
        bodyFatPercentage: 10,
        neckCircumference: 10,
        waistCircumference: 10,
        hipCircumference: 10, 
        weight: 10,
        dateAdded : new Date(2019, 9, 12)
    }
});

it("should render a message indicating the user has no body measurements if no body measurements are provided", () => {
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection}/>);
    const messageElement = screen.getByText("You do not have any body measurements", { exact: false });
    expect(messageElement).toBeTruthy
})

it("should render hip circumference if gender type is female and body measurements are provided", () => {
    bodyMeasurementCollection.bodyMeasurements.push(bodyMeasurement)
    bodyMeasurementCollection.genderTypeName = "Female";
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection}/>);
    const hipCircumferenceElement = screen.getByText("Hip Circumference", { exact: false });
    expect(hipCircumferenceElement).toBeTruthy()
})

it("should not render hip circumference if gender type is male and body measurements are provided", () => {
    bodyMeasurementCollection.bodyMeasurements.push(bodyMeasurement)
    bodyMeasurementCollection.genderTypeName = "male";
    render(<BodyMeasurementList bodyMeasurementCollection={bodyMeasurementCollection}/>);
    const hipCircumferenceElement = screen.queryByText("Hip Circumference", { exact: false });
    expect(hipCircumferenceElement).toBeFalsy()
})