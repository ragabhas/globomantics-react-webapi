import logo from './GloboLogo.png';

type Args = {
    title: string;
}

const Header = ({title}:Args) => {
    return(
        <header className='row mb-4'>
            <div className='col-5'><img src={logo} alt='Logo' className='logo'/></div>
            <div className='col-7 mt-5 subtitle'>{title}</div>
        </header>
        
    );
}

export default Header;