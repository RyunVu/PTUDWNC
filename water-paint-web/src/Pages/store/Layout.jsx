import { Outlet } from 'react-router-dom';

import styles from '../../styles/layout.module.css';

import Navbar from '../../Components/store/Navbar';

export default function Layout() {
    return (
        <>
            <Navbar />
            <div className={`container-fluid ${styles.content}`}>
                <div className="row">
                    <div className={`${styles.main}`}>
                        <Outlet />
                    </div>
                </div>
            </div>
        </>
    );
}
