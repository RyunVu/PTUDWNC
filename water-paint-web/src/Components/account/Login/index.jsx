import React, { useState } from 'react';
import { Link } from 'react-router-dom';

import styles from '../Account.module.scss';
import clsx from 'clsx';

export default function Account() {
    const [toggled, setToggled] = useState(false);

    const handleToggle = () => {
        setToggled(!toggled);
    };

    return (
        <div className={styles.wrapper}>
            <div
                className={toggled ? clsx(styles.container, styles.rightPanelActive) : styles.container}
                id="container">
                <div className={clsx(styles.formContainer, styles.signUpContainer)}>
                    <form action="#" className={styles.register}>
                        <div className={clsx(styles.textPH, 'h1')}>Create Account</div>
                        <div className={styles.socialContainer}>
                            <a href="#" className={styles.social}>
                                <i className="fab fa-facebook-f"></i>
                            </a>
                            <a href="#" className={styles.social}>
                                <i className="fab fa-google-plus-g"></i>
                            </a>
                            <a href="#" className={styles.social}>
                                <i className="fab fa-linkedin-in"></i>
                            </a>
                        </div>
                        <span>or use your email for registration</span>
                        <input type="text" placeholder="Name" className={styles.input} />
                        <input type="email" placeholder="Email" className={styles.input} />
                        <input type="password" placeholder="Password" className={styles.input} />
                        <button className={styles.buttonStyle}>Sign Up</button>
                    </form>
                </div>
                <div className={clsx(styles.formContainer, styles.signInContainer)}>
                    <form action="#" className={styles.login}>
                        <div className={clsx(styles.textPH, 'h1')}>Sign in</div>
                        <div className={styles.socialContainer}>
                            <a href="#" className={styles.social}>
                                <i className="fab fa-facebook-f"></i>
                            </a>
                            <a href="#" className={styles.social}>
                                <i className="fab fa-google-plus-g"></i>
                            </a>
                            <a href="#" className={styles.social}>
                                <i className="fab fa-linkedin-in"></i>
                            </a>
                        </div>
                        <div className={styles.inlineText}>or use your account</div>
                        <input type="username" placeholder="Username" className={styles.input} />
                        <input type="password" placeholder="Password" className={styles.input} />
                        <Link to="/store/forgetpassword" className={styles.link}>
                            Forgot your password?
                        </Link>
                        <button className={styles.buttonStyle}>Sign In</button>
                    </form>
                </div>
                <div className={styles.overlayContainer}>
                    <div className={styles.overlay}>
                        <div className={clsx(styles.overlayPanel, styles.overlayLeft)}>
                            <div className={clsx(styles.textPH, 'h1')}>Welcome Back!</div>
                            <div className={styles.text}>
                                To keep connected with us please login with your personal info
                            </div>
                            <button
                                className={clsx(styles.buttonStyle, styles.ghost)}
                                id="signIn"
                                onClick={handleToggle}>
                                Sign In
                            </button>
                        </div>
                        <div className={clsx(styles.overlayPanel, styles.overlayRight)}>
                            <div className={clsx(styles.textPH, 'h1')}>Hello, Friend!</div>
                            <div className={styles.text}>Enter your personal details and start journey with us</div>
                            <button
                                className={clsx(styles.buttonStyle, styles.ghost)}
                                onClick={handleToggle}
                                id="signUp">
                                Sign Up
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
