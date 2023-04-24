import axios from 'axios';

export async function getUnits() {
    const { data } = await axios.get(
        `${process.env.REACT_APP_API_ENDPOINT}/product/unit?Actived=true&NotActived=false&PageSize=1000&PageNumber=1`,
    );
    if (data.isSuccess) return data.result;
    else return null;
}
