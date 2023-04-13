import { API_URL } from '../Utils/constants';
import { get_api } from './method';

export async function getProductsByQueries(queries) {
    return get_api(`${API_URL}/products?${queries}`);
}
