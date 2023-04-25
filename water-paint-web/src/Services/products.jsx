import axios from 'axios';

export async function getProductsByQueries(queries) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/product?${queries}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function createProduct(product) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/product`, product);

    return data;
}

export async function updateProduct(id, product) {
    const { data } = await axios.put(`${process.env.REACT_APP_API_ENDPOINT}/product/${id}`, product);

    return data;
}

export async function getProductById(id = 0) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/product/${id}`);

    if (data.isSuccess) return data.result;
    else return null;
}

export async function deleteProductById(id) {
    const { data } = await axios.delete(`${process.env.REACT_APP_API_ENDPOINT}/product/${id}`);
    return data;
}

export async function toggleProductActivedStatus(id) {
    const { data } = await axios.post(`${process.env.REACT_APP_API_ENDPOINT}/product/toggleProduct/${id}`);
    console.log(data);
    return data;
}
