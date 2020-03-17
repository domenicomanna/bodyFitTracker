import {get, post, put, destroy} from './baseConfiguration';


const getAllMeasurements = () => get('bodyMeasurements').then(response => response.data);

const BodyMeasurements = {
    getAllMeasurements
}

export default BodyMeasurements;