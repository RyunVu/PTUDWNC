import React from 'react';
import { Navbar as Nb, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <Nb collapseOnSelect expand="sm" bg="white" variant="light" className="border-bottom shadow">
            <div className="container-fluid">
                <Nb.Brand href="/admin">Store</Nb.Brand>
                <Nb.Toggle aria-controls="responsive-navbar-nav" />
                <Nb.Collapse id="responsive-navbar-nav" className="d-sm-inline-flex justify-content-between">
                    <Nav className="mr-auto flwx-grow-1">
                        <Nav.Item>
                            <Link to="/admin/categories" className="nav-link text-dark">
                                Loại sản phẩm
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/admin/products" className="nav-link text-dark">
                                Sản phẩm
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/admin/accounts" className="nav-link text-dark">
                                Tài khoản
                            </Link>
                        </Nav.Item>
                    </Nav>
                </Nb.Collapse>
            </div>
        </Nb>
    );
};

export default Navbar;
