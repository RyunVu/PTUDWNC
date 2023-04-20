import axios from 'axios';

export async function getProductsByQueries(queries) {
    const { data } = await axios.get(`${process.env.REACT_APP_API_ENDPOINT}/product?${queries}`);

    if (data.isSuccess) return data.result;
    else return null;
}
