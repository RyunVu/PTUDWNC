import React from 'react';
import { useToken } from '../../../Utils';
import { Navbar } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCartShopping, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';

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
                            Giỏ hàng <FontAwesomeIcon icon={faCartShopping} />
                        </Link>
                        <Link to="/store/login" className="nav-link text-dark px-2" onClick={removeToken}>
                            Đăng xuất <FontAwesomeIcon icon={faRightFromBracket} />
                        </Link>
                    </>
                )}
            </Navbar>
        </>
    );
}
