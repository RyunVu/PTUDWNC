import styles from './ContactsSidebar.module.scss';
import TelIcon from '../../../assets/icons/Tel.png';
import TellIcon from '../../../assets/icons/Tell.png';
import FaxIcon from '../../../assets/icons/Fax.png';

function ContactsSidebar() {
    return (
        <div>
            <h4 className={styles.heading}>LIÊN HỆ</h4>
            <ul className={styles.list}>
                <li className={styles.item}>
                    <div className={styles.firstItem}>
                        <img src={TellIcon} alt="tell" />
                        <span>VĂN PHÒNG CÔNG TY</span>
                    </div>
                    <ul className={styles.sublist}>
                        <li className={styles.subitem}>
                            <img src={TelIcon} alt="tel" />
                            <span>024.3764.7750</span>
                        </li>
                        <li className={styles.subitem}>
                            <img src={FaxIcon} alt="fax" />
                            <span>024.3764.8035</span>
                        </li>
                    </ul>
                </li>
                <li className={styles.item}>
                    <div className={styles.firstItem}>
                        <img src={TellIcon} alt="tell" />
                        <span>TRUNG TÂM TV & GT SẢN PHẨM</span>
                    </div>
                    <ul className={styles.sublist}>
                        <li className={styles.subitem}>
                            <img src={TelIcon} alt="tel" />
                            <span>024.6287.3660</span>
                        </li>
                        <li className={styles.subitem}>
                            <img src={FaxIcon} alt="fax" />
                            <span>024.3795.7838</span>
                        </li>
                    </ul>
                </li>
                <li className={styles.item}>
                    <div className={styles.firstItem}>
                        <img src={TellIcon} alt="tell" />
                        <span>KINH DOANH - MỞ ĐẠI LÝ</span>
                    </div>
                    <ul className={styles.sublist}>
                        <li className={styles.subitem}>
                            <img src={TelIcon} alt="tel" />
                            <span>024.3212.3882</span>
                        </li>
                        <li className={styles.subitem}>
                            <img src={FaxIcon} alt="fax" />
                            <span>024.3212.3882</span>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    );
}

export default ContactsSidebar;
