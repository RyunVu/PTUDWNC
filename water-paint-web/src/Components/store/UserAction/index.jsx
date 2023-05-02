import React from 'react';
import { useToken } from '../../../Utils';
import { Navbar } from 'react-bootstrap';
import { Link } from 'react-router-dom';

export default function NavRight() {
    const { token, removeToken } = useToken();

    return (
        <>
            <Navbar className="mr-auto flex-grow-2">
                {!token ? (
                    <Link to="/store/login" className="nav-link text-dark">
                        Đăng nhập
                    </Link>
                ) : (
                    <>
                        <Link to="/store/cart" className="nav-link text-dark px-3">
                            Giỏ hàng "thay bằng icon sau"
                        </Link>
                        <Link to="/store/login" className="nav-link text-dark px-2" onClick={removeToken}>
                            Đăng xuất
                        </Link>
                    </>
                )}
            </Navbar>
        </>
    );
}
