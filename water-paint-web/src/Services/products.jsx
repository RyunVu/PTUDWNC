import { get_api } from './method';

export async function getProductsByQueries(queries) {
    return get_api(`${process.env.REACT_APP_API_ENDPOINT}/products?${queries}`);
}
