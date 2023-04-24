import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import { BsSun, BsMoon, BsPaintBucket } from 'react-icons/bs';
import { BiLogOutCircle, BiHome } from 'react-icons/bi';
import { AiOutlineDropbox, AiOutlineUser, AiOutlineRight } from 'react-icons/ai';

import profileImg from '../../../assets/images/avatar.jpg';

export default function Sidebar() {
    const [toggled, setToggled] = useState(false);
    const [theme, setTheme] = useState(false);

    const handleToggle = () => {
        setToggled(!toggled);
    };

    const handleTheme = () => {
        setTheme(!theme);
    };

    useEffect(() => {
        document.body.className = theme ? 'dark' : '';
    }, [theme]);
    return (
        <>
            <input id="menu__toggle" type="checkbox" />
            <label className="menu__btn" for="menu__toggle">
                <span></span>
            </label>
            <nav className={toggled ? 'sidebar' : 'sidebar close'}>
                <header>
                    <div className="image-text">
                        <span className="image">
                            <img src={profileImg} alt="" />
                        </span>

                        <div className="text logo-text">
                            <span className="name">Admin</span>
                            <span className="profession">Store</span>
                        </div>
                    </div>
                    <div className="bx bx-chevron-right toggle" onClick={handleToggle}>
                        <AiOutlineRight />
                    </div>
                </header>

                <div className="menu-bar">
                    <div className="menu">
                        <ul className="menu-links">
                            <li className="nav-link">
                                <Link to="/admin">
                                    <BiHome className="bx bx-home-alt icon" />
                                    <span className="text nav-text">Dashboard</span>
                                </Link>
                            </li>

                            <li className="nav-link">
                                <Link to="/admin/categories">
                                    <AiOutlineDropbox className="bx bx-bar-chart-alt-2 icon" />
                                    <span className="text nav-text">Loại sản phẩm</span>
                                </Link>
                            </li>

                            <li className="nav-link">
                                <Link to="/admin/products">
                                    <BsPaintBucket className="bx bx-bar-chart-alt-2 icon" />
                                    <span className="text nav-text">Sản phẩm</span>
                                </Link>
                            </li>

                            <li className="nav-link">
                                <Link to="/admin/accounts">
                                    <AiOutlineUser className="bx bx-bell icon" />
                                    <span className="text nav-text">Tài khoản</span>
                                </Link>
                            </li>
                        </ul>
                    </div>

                    <div className="bottom-content">
                        <li className="">
                            <Link to="/admin">
                                <BiLogOutCircle className="bx bx-log-out icon" />
                                <span className="text nav-text">Logout</span>
                            </Link>
                        </li>

                        <li className="mode" onClick={handleTheme}>
                            <div className="sun-moon">
                                <BsMoon className="bx bx-moon icon moon" />
                                <BsSun className="bx bx-moon icon sun" />
                            </div>
                            {theme ? (
                                <span className="mode-text text">Dark mode</span>
                            ) : (
                                <span className="mode-text text">Light mode</span>
                            )}

                            <div className="toggle-switch">
                                <span className="switch"></span>
                            </div>
                        </li>
                    </div>
                </div>
            </nav>
        </>
    );
}
