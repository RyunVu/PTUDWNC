import React from 'react';
import { Link } from 'react-router-dom';
import profileImg from '../../../assets/images/avatar.jpg';
import './sidebar.scss';

export default function Sidebar() {
    return (
        <>
            <div className="container">
                <input id="menu__toggle" type="checkbox" />
                <label className="menu__btn" for="menu__toggle">
                    <span></span>
                </label>
                <nav className="sidebar">
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

                        <i className="bx bx-chevron-right toggle"></i>
                    </header>

                    <div className="menu-bar">
                        <div className="menu">
                            <li className="search-box">
                                <i className="bx bx-search icon"></i>
                            </li>

                            <ul className="menu-links">
                                <li className="nav-link">
                                    <Link to="/admin">
                                        <i className="bx bx-home-alt icon"></i>
                                        <span className="text nav-text">Dashboard</span>
                                    </Link>
                                </li>

                                <li className="nav-link">
                                    <Link to="/admin/categories">
                                        <i className="bx bx-bar-chart-alt-2 icon" />
                                        <span className="text nav-text">Loại sản phẩm</span>
                                    </Link>
                                </li>

                                <li className="nav-link">
                                    <Link to="/admin/products">
                                        <i className="bx bx-bar-chart-alt-2 icon" />
                                        <span className="text nav-text">Sản phẩm</span>
                                    </Link>
                                </li>

                                <li className="nav-link">
                                    <Link to="/admin/accounts">
                                        <i className="bx bx-bell icon"></i>
                                        <span className="text nav-text">Tài khoản</span>
                                    </Link>
                                </li>
                            </ul>
                        </div>

                        <div className="bottom-content">
                            <li className="">
                                <a href="#">
                                    <i className="bx bx-log-out icon"></i>
                                    <span className="text nav-text">Logout</span>
                                </a>
                            </li>

                            <li className="mode">
                                <div className="sun-moon">
                                    <i className="bx bx-moon icon moon"></i>
                                    <i className="bx bx-sun icon sun"></i>
                                </div>
                                <span className="mode-text text">Dark mode</span>

                                <div className="toggle-switch">
                                    <span className="switch"></span>
                                </div>
                            </li>
                        </div>
                    </div>
                </nav>

                <section className="home">
                    <div className="text">Dashboard Sidebar</div>
                </section>
            </div>
        </>
    );
}
