import { Navbar as Nb, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <Nb collapseOnSelect expand="sm" bg="white" variant="light" className="border-bottom shadow">
            <div className="container-fluid">
                <Nb.Brand href="/">Logo sơn gì gì đó :v</Nb.Brand>
                <Nb.Toggle aria-controls="responsive-navbar-nav" />
                <Nb.Collapse id="responsive-navbar-nav" className="d-sm-inline-flex justify-content-between">
                    <Nav className="mr-auto flex-grow-1">
                        <Nav.Item>
                            <Link to="/" className="nav-link text-dark">
                                Trang chủ
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/store/category" className="nav-link text-dark">
                                Sản phẩm
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/store/contact" className="nav-link text-dark">
                                Liên hệ
                            </Link>
                        </Nav.Item>
                    </Nav>
                    <Nav className="mr-auto flex-grow-2">
                        <Nav.Item>
                            <Link to="/store/cart" className="nav-link text-dark">
                                Giỏ hàng "thay bằng icon sau"
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/store/login" className="nav-link text-dark">
                                Đăng nhập
                            </Link>
                        </Nav.Item>
                    </Nav>
                </Nb.Collapse>
            </div>
        </Nb>
    );
};

export default Navbar;
