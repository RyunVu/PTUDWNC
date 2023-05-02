import { Navbar as Nb, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import NavRight from '../UserAction';
import Logo from '../../../assets/images/logo.png';
import styles from './Navbar.module.scss'

const Navbar = () => {
    return (
        <Nb collapseOnSelect expand="sm" bg="white" variant="light" className="border-bottom shadow">
            <div className="container-fluid">
                <Nb.Brand href="/">
                    <img src={Logo} alt="logo" className={styles.logo} />
                </Nb.Brand>
                <Nb.Toggle aria-controls="responsive-navbar-nav" />
                <Nb.Collapse id="responsive-navbar-nav" className="d-sm-inline-flex justify-content-between">
                    <Nav className="mr-auto flex-grow-1">
                        <Nav.Item>
                            <Link to="/" className={`nav-link text-dark ${styles.navLink}`}>
                                Trang chủ
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/store/category" className={`nav-link text-dark ${styles.navLink}`}>
                                Sản phẩm
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link to="/store/contact" className={`nav-link text-dark ${styles.navLink}`}>
                                Liên hệ
                            </Link>
                        </Nav.Item>
                    </Nav>
                    <NavRight />
                </Nb.Collapse>
            </div>
        </Nb>
    );
};

export default Navbar;
