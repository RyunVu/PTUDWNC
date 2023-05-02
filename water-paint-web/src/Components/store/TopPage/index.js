import styles from './TopPage.module.scss'

function TopPage({ title }) {
    return (
        <div className={styles.bg}>
            <h3 className={styles.title}>{title}</h3>
        </div>
    );
}

export default TopPage;
