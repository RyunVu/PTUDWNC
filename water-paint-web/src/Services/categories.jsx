import axios from 'axios';

export async function getCategoriesByQueries(queries) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/category?${queries}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function createCategory(category) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/category`, category);

    return data;
}

export async function updateCategory(id, category) {
    const { data } = await axios.put(`${process.env.REACT_APP_API_ENDPOINT}/category/${id}`, category);

    return data;
}

export async function getCategoryById(id = 0) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/category/${id}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function deleteCategoryById(id) {
    const { data } = await axios.delete(`${process.env.REACT_APP_API_ENDPOINT}/category/${id}`);
    return data;
}
