import axios from 'axios';

export async function getUnits() {
    const { data } = await axios.get(
        `${process.env.REACT_APP_API_ENDPOINT}/Unit/unit?Actived=true&NotActived=false&PageSize=1000&PageNumber=1`,
    );
    if (data.isSuccess) return data.result;
    else return null;
}

export async function getUnitsByQueries(queries) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/Unit?${queries}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function createUnit(Unit) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/Unit`, Unit);

    return data;
}

export async function updateUnit(id, Unit) {
    const { data } = await axios.put(`${process.env.REACT_APP_API_ENDPOINT}/Unit/${id}`, Unit);

    return data;
}

export async function getUnitById(id = 0) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/Unit/${id}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function deleteUnitById(id) {
    const { data } = await axios.delete(`${process.env.REACT_APP_API_ENDPOINT}/Unit/${id}`);
    return data;
}

export async function toggleUnitActivedStatus(id) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/Unit/toggleUnit/${id}`);

    return data;
}
