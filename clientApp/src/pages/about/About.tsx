import React from 'react';
import PageTitle from '../../components/pageTitle/PageTitle';
import styles from './about.module.css';

const About = () => {
  return (
    <div className={styles.about}>
      <PageTitle>About This App</PageTitle>
      <p>
        BodyFitTracker is an app that aims to support you on your weight loss or weight gain journey. Through taking
        several different body measurements and with the use of the{' '}
        <a target='_blank' href='https://fitties.com/fat-caliper-plus/body-fat-calculation-methods/us-navy/'>
          US Navy Body Fat Formula
        </a>
        , this app allows you to track your body measurements and body fat percentage.
      </p>

      <p>
        If you're trying to lose weight, then this app will motivate you because you will see your weight drop and
        you'll see smaller circumference measurements in stubborn fat areas such as the waist.
      </p>

      <p>
        If you're trying to gain weight, then most likely you want to put on as much muscle weight as possible while
        minimizing fat gain. BodyFitTracker helps you achieve this because you'll be able to see how your body fat
        percentage changes with your weight gain. If you gain weight, and your body fat percentage remains relatively
        unchanged, then that means the weight gained has been primarily in the form of muscle.
      </p>

      <p>
        Regardless if you are trying to lose or gain weight, the core focus of this app is to help you get in the best
        shape of your life.
      </p>
    </div>
  );
};

export default About;
