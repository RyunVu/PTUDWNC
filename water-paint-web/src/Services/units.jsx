import axios from 'axios';

export async function getUnits() {
    const { data } = await axios.get(
        `${process.env.REACT_APP_API_ENDPOINT}/product/unit?Actived=true&NotActived=false&PageSize=1000&PageNumber=1`,
    );
    if (data.isSuccess) return data.result;
    else return null;
}

export async function getUnitsByQueries(queries) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/product/unit?${queries}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function createUnit(unit) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/product/unit`, unit);

    return data;
}

export async function updateUnit(id, unit) {
    const { data } = await axios.put(`${process.env.REACT_APP_API_ENDPOINT}/product/unit/${id}`, unit);

    return data;
}

export async function getUnitById(id = 0) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/product/unit/${id}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function deleteUnitById(id) {
    const { data } = await axios.delete(`${process.env.REACT_APP_API_ENDPOINT}/product/unit/${id}`);
    return data;
}

export async function toggleUnitActivedStatus(id) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/product/unit/toggleUnit/${id}`);

    return data;
}
