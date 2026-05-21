import React, { useState } from 'react';
import { 
  Box, Typography, Button, Grid, Link as MuiLink, 
  IconButton, InputAdornment, Checkbox, FormControlLabel, Card, CircularProgress, Alert
} from '@mui/material';
import { useForm, Controller } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { FormInputText } from '../../../components/forms/FormInputText';
import { useNavigate } from 'react-router-dom';
import RestaurantIcon from '@mui/icons-material/Restaurant';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import bgImage from '../../../assets/restaurant_kitchen_bg.png';

// Redux & API
import { useDispatch } from 'react-redux';
import { loginSuccess } from '../authSlice';
import { loginAPI } from '../services/authService';
import { getDefaultRouteForRole } from '../../../utils/roleUtils';

const loginSchema = yup.object({
  email: yup.string().email('Enter a valid email').required('Email is required'),
  password: yup.string().min(6, 'Password should be of minimum 6 characters length').required('Password is required'),
  rememberMe: yup.boolean().default(false),
});

type LoginFormInputs = yup.InferType<typeof loginSchema>;

const Login: React.FC = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);

  const { control, handleSubmit } = useForm<LoginFormInputs>({
    resolver: yupResolver(loginSchema) as any,
    defaultValues: {
      email: '',
      password: '',
      rememberMe: false,
    },
  });

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const onSubmit = async (data: LoginFormInputs) => {
    setIsLoading(true);
    setErrorMsg(null);
    try {
      const response = await loginAPI({ email: data.email, password: data.password });
      
      // Save to Redux (which also saves to localStorage via action)
      dispatch(loginSuccess({
        user: response.user,
        token: response.accessToken
      }));
      
      // Redirect based on role
      const defaultRoute = getDefaultRouteForRole(response.user.roleName);
      navigate(defaultRoute, { replace: true });
    } catch (error: any) {
      console.error('Login failed:', error);
      setErrorMsg(error?.response?.data?.message || 'Login failed. Please check your credentials.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Box sx={{ minHeight: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center', backgroundColor: '#f4f6f8', p: { xs: 2, md: 4 } }}>
      <Card sx={{ display: 'flex', maxWidth: 1000, width: '100%', borderRadius: 3, boxShadow: '0 8px 32px rgba(0,0,0,0.08)', overflow: 'hidden' }}>
        <Grid container sx={{ width: '100%' }}>
          {/* Left Side - Image & Branding */}
          <Grid 
            size={{ xs: 12, md: 6 }}
            sx={{
              display: { xs: 'none', md: 'flex' },
              flexDirection: 'column',
              justifyContent: 'flex-end',
              p: 6,
              backgroundImage: `linear-gradient(to bottom, rgba(0,0,0,0.1) 0%, rgba(0,0,0,0.8) 100%), url(${bgImage})`,
              backgroundSize: 'cover',
              backgroundPosition: 'center',
              color: '#ffffff',
            }}
          >
            <Box>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <RestaurantIcon sx={{ fontSize: 32, mr: 1.5 }} />
                <Typography variant="h3" sx={{ fontWeight: 700, m: 0 }}>
                  DineMaster Pro
                </Typography>
              </Box>
              <Typography variant="body1" sx={{ opacity: 0.9, lineHeight: 1.6 }}>
                Streamline your front-of-house, optimize kitchen flow, and elevate the dining experience with our intelligent management platform.
              </Typography>
            </Box>
          </Grid>

          {/* Right Side - Login Form */}
          <Grid 
            size={{ xs: 12, md: 6 }}
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              backgroundColor: '#ffffff',
              p: { xs: 4, md: 6 },
            }}
          >
            <Box sx={{ width: '100%', maxWidth: 400 }}>
              <Typography variant="h2" gutterBottom color="text.primary" sx={{ fontWeight: 700 }}>
                Welcome back
              </Typography>
              <Typography variant="body2" color="text.secondary" sx={{ mb: 4, lineHeight: 1.6 }}>
                Please enter your credentials to access your dashboard.
              </Typography>

              {errorMsg && (
                <Alert severity="error" sx={{ mb: 3 }}>
                  {errorMsg}
                </Alert>
              )}

              <form onSubmit={handleSubmit(onSubmit)}>
                <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
                  <FormInputText
                    name="email"
                    control={control}
                    label="Email Address"
                    type="email"
                  />
                  
                  <FormInputText
                    name="password"
                    control={control}
                    label="Password"
                    type={showPassword ? 'text' : 'password'}
                    slotProps={{
                      input: {
                        endAdornment: (
                          <InputAdornment position="end">
                            <IconButton
                              aria-label="toggle password visibility"
                              onClick={handleClickShowPassword}
                              edge="end"
                            >
                              {showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                          </InputAdornment>
                        ),
                      }
                    }}
                  />

                  <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mt: -1 }}>
                    <Controller
                      name="rememberMe"
                      control={control}
                      render={({ field }) => (
                        <FormControlLabel
                          control={
                            <Checkbox 
                              {...field} 
                              checked={field.value}
                              color="primary" 
                              size="small"
                            />
                          }
                          label={<Typography variant="body2" color="text.secondary">Remember me</Typography>}
                        />
                      )}
                    />
                    <MuiLink href="#" underline="hover" color="primary" sx={{ fontWeight: 600, fontSize: '0.875rem' }}>
                      Forgot password?
                    </MuiLink>
                  </Box>

                  <Button 
                    type="submit" 
                    variant="contained" 
                    color="primary" 
                    size="large"
                    fullWidth
                    disabled={isLoading}
                    sx={{ py: 1.5, fontSize: '1rem', fontWeight: 600, mt: 1 }}
                  >
                    {isLoading ? <CircularProgress size={24} color="inherit" /> : 'Login'}
                  </Button>
                </Box>
              </form>

              <Box sx={{ mt: 4, textAlign: 'center' }}>
                <Typography variant="body2" color="text.secondary">
                  Need an account?{' '}
                  <MuiLink href="#" underline="hover" color="primary" sx={{ fontWeight: 600 }}>
                    Contact Support
                  </MuiLink>
                </Typography>
              </Box>
            </Box>
          </Grid>
        </Grid>
      </Card>
    </Box>
  );
};

export default Login;
