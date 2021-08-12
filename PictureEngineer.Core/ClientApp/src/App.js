import Header from './layouts/Header'
import { BrowserRouter as Router, Switch } from 'react-router-dom'
import Route from './routes/route'
import './assets/stylesheet/app.css'
import './assets/icomoon/style.css'
import Footer from './layouts/Footer'
import React, { Suspense, useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { toast, ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css';
import doSomething from './redux/doSomething'
import Loading from './component/Loading/Loading'
import 'bootstrap/dist/css/bootstrap.min.css';

toast.configure();
export default function App() {

    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(doSomething());
    }, [dispatch])

    return (
        <div className="body-container">
            <Router>
                <Header />
                {/* main */}
                <div className="picture-engineer__main">
                    <Suspense fallback={<Loading></Loading>}>
                        <Switch>
                            <Route />
                        </Switch>
                    </Suspense>
                </div>
                <Footer />
            </Router>
            <ToastContainer autoClose={2000} closeButton={true} hideProgressBar={false} position={'top-right'} />
        </div>
    )
}

