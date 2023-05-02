import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faLocationDot, faPaperPlane, faPhoneVolume } from '@fortawesome/free-solid-svg-icons';
import Logo from '../../../assets/images/logo.png';

// Icons
import FacebookIcon from '../../../assets/icons/FacebookIcon.png';
import YoutubeIcon from '../../../assets/icons/YoutubeIcon.png';
import InIcon from '../../../assets/icons/InIcon.png';
import InstaIcon from '../../../assets/icons/InstaIcon.png';

import styles from './Footer.module.scss';

const Footer = () => {
    return (
        <footer className={`footer ${styles.footer}`}>
            <Container>
                <Row>
                    <Col className={styles.col}>
                        <div className={styles.brand}>
                            <img src={Logo} alt="logo" className={styles.logo} />
                            <h1 className={styles.brandName}>TẬP ĐOÀN SƠN KOVA</h1>
                        </div>
                        <address className={styles.address}>
                            <div className={styles.addressItem}>
                                <FontAwesomeIcon icon={faLocationDot} />
                                <p>Đường CN6 Cụm công nghiệp Từ Liêm, P. Minh Khai, Q. Bắc Từ Liêm, TP Hà Nội</p>
                            </div>
                            <div className={styles.addressItem}>
                                <FontAwesomeIcon icon={faPhoneVolume} />
                                <p>84 2437 647 750</p>
                            </div>
                            <div className={styles.addressItem}>
                                <FontAwesomeIcon icon={faPhoneVolume} />
                                <p>84 2437 648 035</p>
                            </div>
                            <div className={styles.addressItem}>
                                <FontAwesomeIcon icon={faEnvelope} />
                                <p>contact@kovapaint.com.vn</p>
                            </div>
                        </address>
                    </Col>
                    <Col className={styles.col}>
                        <h5 className={styles.registerHeading}>ĐĂNG KÝ NHẬN TIN:</h5>
                        <p className={styles.registerNofi}>
                            Nhập email của bạn để được hỗ trợ nhanh nhất và nhận được những thông tin về khuyến mại sớm
                            nhất!
                        </p>
                        <div className={styles.sendMail}>
                            <input type="email" className={styles.sendMailInput} />
                            <button className={styles.sendMailButton}>
                                <FontAwesomeIcon icon={faPaperPlane} />
                            </button>
                        </div>
                        <div className={styles.socialIcons}>
                            <img src={FacebookIcon} alt="facebook" />
                            <img src={YoutubeIcon} alt="facebook" />
                            <img src={InIcon} alt="facebook" />
                            <img src={InstaIcon} alt="facebook" />
                        </div>
                    </Col>
                    <Col className={styles.col}>
                        <h5>FANPAGE:</h5>
                        <iframe
                            title="fanpage"
                            src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2FSonkova.hn%2F&tabs=timeline&width=340&height=331&small_header=false&adapt_container_width=true&hide_cover=false&show_facepile=false&appId"
                            width="340"
                            height="130"
                            style={{
                                border: 'none',
                                overflow: 'hidden',
                            }}
                            allow="autoplay; clipboard-write; encrypted-media; picture-in-picture; web-share"></iframe>
                    </Col>
                </Row>
                <div className={styles.divider}></div>
                <h5 className={styles.copyright}>@2023 Bản quyền thuộc Tập đoàn Sơn Kova - Design by Nanoweb</h5>
            </Container>
        </footer>
    );
};

export default Footer;
