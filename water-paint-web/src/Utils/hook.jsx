import { useContext, useState } from 'react';
import Context from './context';

const useStore = () => {
    const [states, dispatch] = useContext(Context);
    return [states, dispatch];
};

const useToken = () => {
    const getToken = () => {
        const tokenString = localStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        return userToken;
    };

    const [token, setToken] = useState(getToken());

    const saveToken = (userToken) => {
        localStorage.setItem('token', JSON.stringify(userToken));
        setToken(userToken.token);
        window.location.reload();
    };

    const removeToken = () => {
        localStorage.removeItem('token');
        window.location.assign('/');
    };

    return {
        setToken: saveToken,
        token,
        removeToken,
    };
};

export { useStore, useToken };
