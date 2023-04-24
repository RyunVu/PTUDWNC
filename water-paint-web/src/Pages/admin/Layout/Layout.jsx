import { Outlet } from 'react-router-dom';
import Sidebar from '../../../Components/admin/Sidebar/sidebar';

import './layout.css';

const AdminLayout = () => {
    return (
        <>
            <div className="container">
                <Sidebar />
                <div className="home py-3 px-4">
                    <Outlet />
                </div>
            </div>
        </>
    );
};

export default AdminLayout;
