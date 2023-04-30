import React from 'react';
import clsx from 'clsx';
import styles from '../Layout/layout.module.scss';
import { FaUserAlt, FaUserFriends, FaStoreAlt } from 'react-icons/fa';
import { SlNotebook } from 'react-icons/sl';
import { BiCategoryAlt, BiCategory } from 'react-icons/bi';

export default function Dashboard() {
    return (
        <>
            <div className={styles.text}>Dashboard</div>
            <div className="row">
                <div className="col s6">
                    <div style={{ padding: 35 }} align="center" className="card">
                        <div className="row">
                            <div className="left card-title">
                                <b>User Management</b>
                            </div>
                        </div>

                        <div className="row">
                            <a href="#!">
                                <div style={{ padding: 30 }} className="grey lighten-3 col s5 waves-effect">
                                    <FaUserAlt className="largeIcon" style={{ fontSize: '48px' }} />
                                    {/* <i className="indigo-text text-lighten-1 large material-icons">person</i> */}
                                    <span className="indigo-text text-lighten-1">
                                        <h5>Seller</h5>
                                    </span>
                                </div>
                            </a>
                            <div className="col s1">&nbsp;</div>
                            <div className="col s1">&nbsp;</div>

                            <a href="#!">
                                <div style={{ padding: 30 }} className="grey lighten-3 col s5 waves-effect">
                                    <FaUserFriends className="largeIcon" style={{ fontSize: '48px' }} />
                                    <span className="indigo-text text-lighten-1">
                                        <h5>Customer</h5>
                                    </span>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>

                <div className="col s6">
                    <div style={{ padding: 35 }} align="center" className="card">
                        <div className="row">
                            <div className="left card-title">
                                <b>Product Management</b>
                            </div>
                        </div>
                        <div className="row">
                            <a href="#!">
                                <div style={{ padding: 30 }} className="grey lighten-3 col s5 waves-effect">
                                    <FaStoreAlt className="largeIcon" style={{ fontSize: '48px' }} />
                                    <span className="indigo-text text-lighten-1">
                                        <h5>Product</h5>
                                    </span>
                                </div>
                            </a>

                            <div className="col s1">&nbsp;</div>
                            <div className="col s1">&nbsp;</div>

                            <a href="#!">
                                <div style={{ padding: 30 }} className="grey lighten-3 col s5 waves-effect">
                                    <SlNotebook className="largeIcon" style={{ fontSize: '48px' }} />
                                    <span className="indigo-text text-lighten-1">
                                        <h5>Orders</h5>
                                    </span>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>

                <div className="col s6">
                    <div style={{ padding: 35 }} align="center" className="card">
                        <div className="row">
                            <div className="left card-title">
                                <b>Category Management</b>
                            </div>
                        </div>
                        <div className="row">
                            <a href="#!">
                                <div style={{ padding: 30 }} className="grey lighten-3 col s5 waves-effect">
                                    <BiCategoryAlt className="largeIcon" style={{ fontSize: '48px' }} />
                                    <span className="indigo-text text-lighten-1">
                                        <h5>Category</h5>
                                    </span>
                                </div>
                            </a>
                            <div className="col s1">&nbsp;</div>
                            <div className="col s1">&nbsp;</div>

                            <a href="#!">
                                <div style={{ padding: 30 }} className="grey lighten-3 col s5 waves-effect">
                                    <BiCategory className="largeIcon" style={{ fontSize: '48px' }} />
                                    <span className="truncate indigo-text text-lighten-1">
                                        <h5>Sub Category</h5>
                                    </span>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
