import React from 'react';
import { useToken } from '../../../Utils';
import { Navbar } from 'react-bootstrap';
import { Link } from 'react-router-dom';

export default function NavRight() {
    const { token, removeToken } = useToken();

    return (
        <>
            {!token ? (
                <Navbar className="mr-auto flex-grow-2">
                    <Link to="/store/login" className="nav-link text-dark">
                        Đăng nhập
                    </Link>
                </Navbar>
            ) : null}
            {token ? (
                <Navbar className="mr-auto flex-grow-2">
                    <Link to="/store/cart" className="nav-link text-dark px-3">
                        Giỏ hàng "thay bằng icon sau"
                    </Link>
                    <Link to="/store/login" className="nav-link text-dark px-2" onClick={removeToken}>
                        Đăng xuất
                    </Link>
                </Navbar>
            ) : null}
        </>
    );
}
