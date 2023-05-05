import React, { useEffect, useRef, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { faInfoCircle } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import styles from '../Account.module.scss';
import clsx from 'clsx';

import { useToken } from '../../../Utils/hook';

const USER_REGEX = /^[A-z][A-z0-9-_]{3,23}$/;
const EMAIL_REGEX = /^[a-z0-9](\.?[a-z0-9]){0,}@g(oogle)?mail\.com$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;

export default function Account() {
    // Ref componets
    const userRef = useRef();

    // State componets

    // UI
    const [toggled, setToggled] = useState(false);

    const handleToggle = () => {
        setToggled(!toggled);
    };

    // Register

    const [usernameR, setUser] = useState('');
    const [validName, setValidName] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [email, setEmail] = useState('');
    const [validEmail, setValidEmail] = useState(false);
    const [emailFocus, setEmailFocus] = useState(false);

    const [pwd, setPwd] = useState('');
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [errMsg, setErrMsg] = useState('');
    const [name, setName] = useState('');
    const [phone, setPhone] = useState('');
    const [address, setAddress] = useState('');

    const validateSignUp = () => {
        if (usernameR === '' || pwd === '' || email === '') {
            setErrMsg('Vui lòng nhập đầy đủ thông tin tài khoản!');
            return false;
        }
        return true;
    };

    useEffect(() => {
        userRef.current.focus();
    }, []);

    useEffect(() => {
        setValidName(USER_REGEX.test(usernameR));
    }, [usernameR]);

    useEffect(() => {
        setValidEmail(EMAIL_REGEX.test(email));
    }, [email]);

    useEffect(() => {
        setValidPwd(PWD_REGEX.test(pwd));
    }, [pwd]);

    useEffect(() => {
        setErrMsg('');
    }, [usernameR, email, pwd]);

    const handleRegister = async (e) => {
        e.preventDefault();
        if (validateSignUp()) {
            const data = {
                name: name,
                email: email,
                password: pwd,
                username: usernameR,
                phone: phone,
                address: address,
                listRoles: [3],
            };
            fetch(`${process.env.REACT_APP_API_ENDPOINT}/account/Register`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data),
            }).then((response) => response.json());
        }
        window.location.reload();
    };

    // Login
    const [errorMessage, setErrorMessage] = useState('');

    const [username, setUsername] = useState();
    const [password, setPassword] = useState();

    const { setToken } = useToken();
    const validateSignIn = () => {
        if (username === '' || password === '') {
            setErrorMessage('Vui lòng nhập đầy đủ thông tin tài khoản!');
            return false;
        }
        return true;
    };

    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        if (validateSignIn()) {
            fetch(`${process.env.REACT_APP_API_ENDPOINT}/account`, {
                method: 'POST',
                body: JSON.stringify({ username: username, password: password }),
                headers: {
                    'Content-Type': 'application/json',
                },
            })
                .then((response) => {
                    if (response.status !== 200) {
                        setErrorMessage('Tài khoản hoặc mật khẩu sai!');
                        throw new Error('Invalid username or password');
                    }
                    return response.json();
                })
                .then((responseToken) => {
                    if (responseToken.token) {
                        console.log(responseToken.token);
                        setToken('bearer ' + responseToken.token);
                        navigate('/');
                    } else {
                        setErrorMessage(responseToken.error_description);
                    }
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        }
    };

    return (
        <div className={styles.wrapper}>
            <div
                className={toggled ? clsx(styles.container, styles.rightPanelActive) : styles.container}
                id="container">
                {/*  register */}
                <div className={clsx(styles.formContainer, styles.signUpContainer)}>
                    <form action="#" className={styles.register} onSubmit={handleRegister}>
                        {errMsg ? <div className="text-center text-danger">{errMsg}</div> : null}
                        <div className={clsx(styles.textPH, 'h1')}>Create Account</div>

                        <input
                            type="text"
                            placeholder="Name"
                            className={styles.input}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />

                        <input
                            type="email"
                            placeholder="Email"
                            className={styles.input}
                            onChange={(e) => setEmail(e.target.value)}
                            value={email}
                            required
                            aria-invalid={validEmail ? 'false' : 'true'}
                            aria-describedby="emailnote"
                            onFocus={() => setEmailFocus(true)}
                            onBlur={() => setEmailFocus(false)}
                        />
                        <p
                            id="emailnote"
                            className={emailFocus && !validEmail ? styles.instructions : styles.offscreen}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            Email not valid.
                        </p>

                        <input
                            type="text"
                            placeholder="Username"
                            className={styles.input}
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setUser(e.target.value)}
                            required
                            aria-invalid={validName ? 'false' : 'true'}
                            aria-describedby="uidnote"
                            onFocus={() => setUserFocus(true)}
                            onBlur={() => setUserFocus(false)}
                        />
                        <p
                            id="uidnote"
                            className={userFocus && usernameR && !validName ? styles.instructions : styles.offscreen}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            4 to 24 characters.
                            <br />
                            Must begin with a letter.
                        </p>

                        <input
                            type="password"
                            placeholder="Password"
                            className={styles.input}
                            onChange={(e) => setPwd(e.target.value)}
                            value={pwd}
                            required
                            aria-invalid={validPwd ? 'false' : 'true'}
                            aria-describedby="pwdnote"
                            onFocus={() => setPwdFocus(true)}
                            onBlur={() => setPwdFocus(false)}
                        />
                        <p id="pwdnote" className={pwdFocus && !validPwd ? styles.instructions : styles.offscreen}>
                            <FontAwesomeIcon icon={faInfoCircle} />
                            8 to 24 characters.
                            <br />
                            Must include uppercase and lowercase letters, a number and a special character.
                            <br />
                            Allowed special characters: <span aria-label="exclamation mark">!</span>{' '}
                            <span aria-label="at symbol">@</span> <span aria-label="hashtag">#</span>{' '}
                            <span aria-label="dollar sign">$</span> <span aria-label="percent">%</span>
                        </p>

                        <input
                            type="text"
                            placeholder="Phone"
                            className={styles.input}
                            onChange={(e) => setPhone(e.target.value)}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Address"
                            className={styles.input}
                            onChange={(e) => setAddress(e.target.value)}
                            required
                        />

                        <button
                            type="submit"
                            className={styles.buttonStyle}
                            disabled={!validName || !validPwd || !validEmail ? true : false}>
                            Sign Up
                        </button>
                    </form>
                </div>

                {/* login */}
                <div className={clsx(styles.formContainer, styles.signInContainer)}>
                    <form action="#" className={styles.login} onSubmit={handleLogin}>
                        <div className={clsx(styles.textPH, 'h1')}>Sign in</div>
                        {errorMessage ? <div className="text-center text-danger">{errorMessage}</div> : null}

                        <input
                            type="username"
                            placeholder="Username"
                            className={styles.input}
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            autoFocus
                            required
                        />
                        <input
                            type="password"
                            placeholder="Password"
                            className={styles.input}
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                        <Link to="/store/forgetpassword" className={styles.link}>
                            Forgot your password?
                        </Link>
                        <button className={styles.buttonStyle} type="submit">
                            Sign In
                        </button>
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
