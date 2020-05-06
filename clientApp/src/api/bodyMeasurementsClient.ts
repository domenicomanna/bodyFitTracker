import {get, post, put, destroy} from './baseConfiguration';


const getAllMeasurements = () => get('bodyMeasurements').then(response => response.data);

const bodyMeasurementsClient = {
    getAllMeasurements
}

export default bodyMeasurementsClient;